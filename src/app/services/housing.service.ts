import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ICategory } from '../home/ICategory.interface';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class HousingService {

  constructor(private http:HttpClient) { }


  getAllCategories(): Observable<ICategory[]> {
    return this.http.get('data/categories.json').pipe(
      map(data => {
        const categoriesArray: Array<ICategory> = [];
        for (const id in data) {
          if (data.hasOwnProperty(id)) {
            categoriesArray.push(data[id]);
          }
        }
        return categoriesArray;
      })
    );
  }
}
