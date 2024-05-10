import { Component, Input } from '@angular/core';
import { ListModel } from '../models/list.model';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent {
  @Input() list!: ListModel;

  constructor(private apiService: ApiService) {}

  deleteList() {
    this.apiService.deleteList(this.list.id, this.list.listName).subscribe(() => {
      console.log('List deleted successfully');
      // Здесь нужно обновить локальный список, убирая удалённый
    });
  }

  editList(newName: string) {
    const oldName = this.list.listName;
    this.list.listName = newName;
    this.apiService.updateList(this.list, oldName).subscribe(() => {
      console.log('List updated successfully');
    });
  }
}
