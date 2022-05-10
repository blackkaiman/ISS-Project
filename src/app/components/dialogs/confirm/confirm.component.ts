import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ConfirmDialogData } from 'src/app/models/confirm-dialog-data';
import { RouteValues } from 'src/app/models/constants';

@Component({
  selector: 'app-confirm',
  templateUrl: './confirm.component.html',
  styleUrls: ['./confirm.component.scss']
})
export class ConfirmComponent implements OnInit {

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: ConfirmDialogData,
    private router: Router
  ) { }

  ngOnInit(): void {
  }
  
}
