import {Component, Inject} from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import {DialogData} from '../office-list.component'

@Component({
    selector: 'delete-dialog',
    templateUrl: 'delete-dialog.component.html',
})

export class DeleteDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<DeleteDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {}

  UserClick(value:boolean): void {
    this.dialogRef.close(value);
  }
}
