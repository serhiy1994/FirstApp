import { ListModel } from "./list.model";

export interface CardModel {
    id: number;
    cardName: string;
    description: string;
    date: Date;
    priority: string;
    listId: number;
    list: ListModel;
  }
  