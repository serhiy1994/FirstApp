import { Component } from '@angular/core';
import { ListModel } from './models/list.model';
import { ApiService } from './api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  lists: ListModel[] = [];

  constructor(private apiService: ApiService) {
    this.loadLists();
  }

  loadLists() {
    this.apiService.getLists().subscribe(lists => {
      this.lists = lists;
    });
  }

  addList(newListName: string) {
    const newList = new ListModel();
    newList.listName = newListName;
    this.apiService.addList(newList).subscribe(list => {
      this.lists.push(list);
      console.log('List added successfully');
    });
  }
}
