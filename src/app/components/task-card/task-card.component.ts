import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Task, ITask } from 'src/app/models/task';
import { MatSnackBar } from '@angular/material/snack-bar';
import {
	AbstractControl,
	FormControl,
	FormGroup,
	Validators,
} from '@angular/forms';
import { TaskService } from 'src/app/services/task.service';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { DialogService } from 'src/app/services/dialog.service';
import { Router } from '@angular/router';
import { ConfirmDialogData } from 'src/app/models/confirm-dialog-data';
import { RouteValues } from 'src/app/models/constants';
import { UserDetails } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import { TaskPresenceService } from 'src/app/services/taskpresence.service';
import { ITaskPresence } from 'src/app/models/taskpresence';
import { L } from '@angular/cdk/keycodes';
import { AuthenticationService } from 'src/app/services/auth.service';
import { loginUser } from 'src/app/models/loginUser';

@Component({
	selector: 'app-task-card',
	templateUrl: './task-card.component.html',
	styleUrls: ['./task-card.component.scss'],
})
export class TaskCardComponent implements OnInit {
	TaskForm: FormGroup;

	TaskCard: ITask = {
		title: '',
	};

	dataSource: UserDetails[] = [];
	displayedColumns = ['Name','Actions'];
	createNewTask: boolean = false;
	urlID: number = -1;
	pageTitle: string = '';
	isParamNan: boolean = this.testNaN(this.urlID);

	assignedEmployees: UserDetails[];
	
	constructor(
		private TaskService: TaskService,
		private _snackBar: MatSnackBar,
		private route: ActivatedRoute,
		private dialogService: DialogService,
		private router: Router,
		private userService:UserService,
		private taskPresenceService:TaskPresenceService,
		private authService:AuthenticationService,

	) {}

	assignedEmployeesId:number[]=[];
	assignedEmployeesId1:number[]=[];
	removedEmployeesId:number[]=[];

	initTaskForm(): void {
		this.TaskForm = new FormGroup({
			titleControl: new FormControl(
				{ value: '', disabled: false },
				[
					Validators.required,
					Validators.pattern('^[a-zA-Z]+[a-zA-Z ]*'),
					Validators.maxLength(24),
				]
			),
		});
	}

	testNaN(param: number) {
		return isNaN(param) ? true : false;
	}
	addToTask(id:number):void{
		this.assignedEmployeesId.push(id);
		this.removedEmployeesId=this.removedEmployeesId.filter(el=> el === id);
	}
	removeFromTask(id:number):void{
		this.assignedEmployeesId=this.assignedEmployeesId.filter(el=> el !== id);
		this.removedEmployeesId.push(id);
	}
	isInAssigned(id:number):boolean{
		return this.assignedEmployeesId.find(el => el === id) !== undefined;
	}
	ngOnInit(): void {
		this.initTaskForm();
		this.urlID = parseInt(this.route.snapshot.paramMap.get('Id'));

		this.taskPresenceService.getPresenceForTask(this.urlID).subscribe(result=>{
			this.assignedEmployees = result;
			result.forEach(el => this.assignedEmployeesId.push(el.id));
			this.assignedEmployeesId1=this.assignedEmployeesId;
		})
		this.userService.getAllUsers().subscribe(result=>{
			this.dataSource = result;
		})
		
		if (!this.urlID) {
			this.pageTitle = 'New Task';
			this.createNewTask = true;
			this.TaskForm.controls.titleControl.enable();
			return;
		}

		this.TaskService.getTask(this.urlID).subscribe(
			(result: ITask) => {
				this.TaskCard.id = result.id;
				this.TaskCard.title = result.title;
				this.TaskForm.patchValue({
					['titleControl']: result.title,
				});
			},
			(error) => {
				{
					if (error.status != undefined) {
						this._snackBar.open(
							`Error status ${error.status}: ${error.message}`,
							'',
							{
								duration: 5000,
							}
						);
					}
				}
			}
		);
	}

	openDiscardDialog(): Observable<boolean> {
		return this.dialogService.confirmDialog({
			title: 'Confirm discard.',
			message: 'Are you sure you want to revert your changes?',
			confirmText: 'Yes',
			cancelText: 'No',
		});
	}

	openDeleteDialog(): Observable<boolean> {
		return this.dialogService.confirmDialog({
			title: 'Confirm deletion.',
			message: 'Are you sure you want to delete this Task?',
			confirmText: 'Yes',
			cancelText: 'No',
		});
	}


	onDelete() {
		if (this.TaskCard.hasEvents) {
			return;
		}
		this.openDeleteDialog().subscribe((result) => {
			if (result) {
				this.TaskService
					.deleteTask(this.TaskCard.id!)
					.subscribe(
						(result) => {
							this._snackBar.open('Task was deleted.', '', {
								duration: 3000,
							});
							this.router.navigate([
								RouteValues.ADMINISTRATE_TASKS,
							]);
						},
						(error) => {
							this._snackBar.open(
								`Error status ${error.status}: ${error.message}`,
								'',
								{
									duration: 5000,
								}
							);
						}
					);
			}
		});
	}
	newPresence:ITaskPresence={
		userId:4,
		taskId:1,
	};

	
	onSubmit() {
		if (!this.TaskForm.valid) {
			return;
		}

		this.TaskCard.title = this.TaskForm.get('titleControl')!
			.value as string;
		if (!this.urlID) {
			this.TaskService.addNewTask(this.TaskCard).subscribe({
				next: (_: ITask) => {
					this.newPresence.taskId = _.id;
					this.assignedEmployeesId.forEach(el=>{
						this.newPresence.userId=el;
						this.taskPresenceService.addNewPresence(this.newPresence).subscribe();})
					this.removedEmployeesId.forEach(el=> {
						this.newPresence.userId=el;
						this.taskPresenceService.deletePresence(this.newPresence.taskId,el).subscribe();})
					
					this._snackBar.open('Task was added.', '', {
						duration: 1500,
					});

					this.router.navigate([RouteValues.ADMINISTRATE_TASKS]);
				},
				error: (error: Error) => {
					this._snackBar.open(
						error.message,
						'',
						{
							duration: 5000,
						}
					);
				}
			});
		} else {
			this.TaskService.updateTask(this.TaskCard).subscribe(
				(result) => {
					
					this.newPresence.taskId = this.urlID;
					this.assignedEmployeesId = this.assignedEmployeesId.filter(el=> this.assignedEmployeesId1.find(
						el2=> {el===el2} 
					) === undefined);
		
					this.assignedEmployeesId.forEach(el=>{
						this.newPresence.userId=el;
						this.taskPresenceService.addNewPresence(this.newPresence).subscribe();})
					
					this.removedEmployeesId = this.removedEmployeesId.filter(el=> this.removedEmployeesId.find(
							el2=> { el===el2}
							)=== undefined);
		
					this.removedEmployeesId.forEach(el=> {
						this.newPresence.userId=el;
						this.taskPresenceService.deletePresence(this.newPresence.taskId,el).subscribe();})
					
					this._snackBar.open('Task was updated.', '', {
						duration: 3000,
					});
					this.router.navigate([RouteValues.ADMINISTRATE_TASKS]);
				},
				(error) => {
					this._snackBar.open(
						`Error status ${error.status}: ${error.message}`,
						'',
						{
							duration: 5000,
						}
					);
				}
			);
		}
	}
}
