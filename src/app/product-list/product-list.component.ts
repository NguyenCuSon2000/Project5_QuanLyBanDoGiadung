import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  public categoryId: number;
  constructor(private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.categoryId = +this.route.snapshot.params['id']

    this.route.params.subscribe(
      (params) => {
        this.categoryId = +params['id'];
      }
    )
  }

  selectNextPage() {
    this.categoryId += 1;
    this.router.navigate(['product-list', this.categoryId]);

  }
}
