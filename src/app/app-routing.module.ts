import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TasksTableComponent } from './components/tasks-table/tasks-table.component';
import { TaskCardComponent } from './components/task-card/task-card.component';
import { LoginCardComponent } from './components/login-card/login-card.component';
import { RegisterCardComponent } from './components/register-card/register-card.component';
import { AdminGuard, AuthGuard, UserGuard } from './helpers/auth.guard';
import { RouteValues } from './models/constants';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NotFoundComponent } from './components/not-found/not-found.component';


const routes: Routes = [
  { path: RouteValues.LOGIN, component: LoginCardComponent },
  { path: RouteValues.REGISTER, component: RegisterCardComponent },
  { path: RouteValues.ADMINISTRATE_TASKS, component: TasksTableComponent, canActivate: [AuthGuard, AdminGuard]},
  { path: RouteValues.Task_NEW, component: TaskCardComponent, canActivate: [AuthGuard, AdminGuard]},
  { path: RouteValues.Task_ID,component: TaskCardComponent, canActivate: [AuthGuard, AdminGuard]},
  { path: RouteValues.DASHBOARD, component: DashboardComponent, canActivate: [AuthGuard, UserGuard] },
  { path: RouteValues.NOT_FOUND,component:NotFoundComponent,},
  { path: RouteValues.DEFAULT, redirectTo: RouteValues.LOGIN, pathMatch: 'full'}
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})

export class AppRoutingModule { }
