import { Component, Input } from '@angular/core';
import { ApiService } from '../api.service';
import { ListModel } from '../models/list.model';

@Component({
  selector: 'app-edit-list-sidebar',
  templateUrl: './edit-list-sidebar.component.html',
  styleUrls: ['./edit-list-sidebar.component.css']
})
export class EditListSidebarComponent {
  @Input() list!: ListModel;
  originalName!: string;

  constructor(private apiService: ApiService) {}

  ngOnInit() {
    this.originalName = this.list.listName;
  }

  save() {
    if (this.list.listName !== this.originalName) {
      this.apiService.updateList(this.list, this.originalName).subscribe(
        () => console.log('List updated successfully'),
        (error: any) => console.error('Error updating list', error)
      );
    }
  }

  cancel() {
    this.list.listName = this.originalName;
  }
}
