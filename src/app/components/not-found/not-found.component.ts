import { Component, OnInit } from '@angular/core';
import { RouteValues } from '../../models/constants';

@Component({
	selector: 'app-not-found',
	templateUrl: './not-found.component.html',
	styleUrls: ['./not-found.component.scss'],
})

export class NotFoundComponent implements OnInit {
	redirectURL: string = '/' + RouteValues.DASHBOARD;

	constructor() {}

	ngOnInit(): void {}
}
