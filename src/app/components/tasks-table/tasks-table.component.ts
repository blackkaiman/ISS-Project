import { Component, OnInit } from '@angular/core';
import { Task} from 'src/app/models/task';
import { EndpointsService } from 'src/app/services/endpoints.service';
import { DomSanitizer } from '@angular/platform-browser';
import { ConfirmComponent } from '../dialogs/confirm/confirm.component';
import { Observable, sequenceEqual } from 'rxjs';
import { DialogService } from 'src/app/services/dialog.service';
import { Router } from '@angular/router';
import { RouteValues } from 'src/app/models/constants';
import { AuthenticationService } from 'src/app/services/auth.service';

@Component({
	selector: 'app-tasks-table',
	templateUrl: './tasks-table.component.html',
	styleUrls: ['./tasks-table.component.scss'],
})
export class TasksTableComponent implements OnInit {
	dataSource: Task[] = [];
	displayedColumns = [ 'Task Description', 'Actions'];
	isLoading: boolean = true;

	constructor(
		private endpointsService: EndpointsService,
		private sanitizer: DomSanitizer,
		private dialogService: DialogService,
		private router: Router,
		private authService:AuthenticationService,
	) {}

	ngOnInit(): void {
		this.endpointsService.getTasks().subscribe((Task) => {
			this.dataSource = Task.result;
			this.isLoading = false;
		});
	}
	onLogout():void {
		this.authService.logout();
		this.router.navigate([RouteValues.LOGIN]);
	}
	deleteTask(TaskId: number): void {
		this.openChangeRoleDialog().subscribe((result) => {
			if (result)
				this.endpointsService
					.deleteTask(TaskId)
					.subscribe(() => {
						this.dataSource =
							this.dataSource.filter(
								(Task) =>
									Task.id != TaskId
							);

						this.router.navigate([
							RouteValues.ADMINISTRATE_TASKS,
						]);
					});
		});
	}


	openChangeRoleDialog(): Observable<boolean> {
		return this.dialogService.confirmDialog({
			title: 'Delete Task',
			message: 'Are you sure you want to delete this task?',
			confirmText: 'Yes',
			cancelText: 'No',
		});
	}

	editTask(TaskId: number): void {
		this.router.navigate([RouteValues.Task, TaskId], {
			queryParams: { editMode: true },
		});
	}

	onTaskRowClick(selectedRow: any): void {
		const TaskId: number = selectedRow.id;
		this.router.navigate([RouteValues.Task, TaskId]);
	}

	navigatetoTaskNew(): void {
		this.router.navigate([RouteValues.Task_NEW]);
	}
}
