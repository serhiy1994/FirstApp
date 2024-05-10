import { Component, Input, OnInit } from '@angular/core';

import { CardModel } from '../models/card.model';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent implements OnInit {
  @Input() card: CardModel | undefined;

  constructor() { }

  ngOnInit(): void {
  }

  editCard(): void {
    // Logic to edit the card details
  }

  deleteCard(): void {
    // Logic to delete the card
  }

  //other methods if neccessary
}
