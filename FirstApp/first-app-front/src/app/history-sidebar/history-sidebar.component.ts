import { Component, OnInit } from '@angular/core';
import { ActivityModel } from '../models/activity.model';

@Component({
  selector: 'app-history-sidebar',
  templateUrl: './history-sidebar.component.html',
  styleUrls: ['./history-sidebar.component.css']
})
export class HistorySidebarComponent implements OnInit {
  history: ActivityModel[] = [];

  constructor() { }

  ngOnInit(): void {
  }

  // Method to add history entries, possibly called from parent component
  addHistoryEntry(entry: ActivityModel) {
    this.history.unshift(entry); // Adds to the top of the history list
  }

    //other methods if neccessary
}
