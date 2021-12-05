import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from 'src/app/core/base-component';

@Component({
  selector: 'app-blog-list',
  templateUrl: './blog-list.component.html',
  styleUrls: ['./blog-list.component.css']
})
export class BlogListComponent extends BaseComponent implements OnInit {
  public list_blog: any;
  public page = 1;
  public pageSize = 3;
  public totalItems:any;
  constructor(injector: Injector, private route: ActivatedRoute) {
    super(injector);
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this._api.post('/api/TinTuc/search',{page: this.page, pageSize: this.pageSize, tieuDe: ""}).takeUntil(this.unsubscribe).subscribe(res => {
        this.list_blog = res.data;
        this.totalItems = res.totalItems;
        setTimeout(() => {
          this.loadScripts();
        });
      });
    });
  }
  loadPage(page) { 
    this._route.params.subscribe(params => {
      this._api.post('/api/TinTuc/search', { page: page, pageSize: this.pageSize, tieuDe: ""}).takeUntil(this.unsubscribe).subscribe(res => {
        this.list_blog = res.data;
        this.totalItems = res.totalItems;
        }, err => { });       
   });
  }
}
