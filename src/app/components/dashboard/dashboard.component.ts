import { Time } from '@angular/common';
import { AfterViewInit, Component, DoCheck, ElementRef, OnChanges, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Task } from 'src/app/models/task';
import { RouteValues } from 'src/app/models/constants';
import { loginUser } from 'src/app/models/loginUser';
import { User, UserUpdatePresence } from 'src/app/models/user';
import { AuthenticationService } from 'src/app/services/auth.service';
import { TaskService } from 'src/app/services/task.service';
import { DialogService } from 'src/app/services/dialog.service';
import { EndpointsService } from 'src/app/services/endpoints.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';
import { UserService } from 'src/app/services/user.service';


@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
    dataSource: Task[] = [];
	displayedColumns = ['Task Description'];
	isLoading: boolean = true;
	isLogged:boolean=false;
	currentUser!:loginUser;
	loginTime:Time;
	constructor(
		private endpointsService: EndpointsService,
		private sanitizer: DomSanitizer,
		private dialogService: DialogService,
		private router: Router,
		private userService : UserService,
		private authService: AuthenticationService,
		private TaskService: TaskService,
	) {}

	ngOnInit(): void {
		this.endpointsService.getTasks().subscribe((Task) => {
			this.dataSource = Task.result;
			this.isLoading = false;
		});
		this.currentUser=this.authService.currentUserValue;
		// this.TaskService.getTasksForUser(this.currentUser.id).subscribe((Task) => {
		
		// 	console.log(Task);
		// 	this.isLoading = false
		// })
	}
	user:UserUpdatePresence={
		id:0,
		isPresent:false,
	}
	onLogin():void {
		this.isLogged=true;
		this.user.id=this.currentUser.id;
		this.user.isPresent = this.isLogged;
		this.userService.putUserPresence(this.user).subscribe(result=>{
		})
		this.loginTime={hours:0,minutes:0};
	}
	onLogout():void {
		this.isLogged=false;
		this.user.id=this.currentUser.id;
		this.user.isPresent = this.isLogged;
		this.userService.putUserPresence(this.user).subscribe(result=>{
			
		})
		this.authService.logout();
		this.router.navigate([RouteValues.LOGIN]);
	}
}
