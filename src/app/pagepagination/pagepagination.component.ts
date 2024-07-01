import { Component, Input, OnInit} from '@angular/core';
import { PageService } from '../service/pagination/page.service';
import { Router } from '@angular/router';
import { FormControl, FormGroup, NgForm, FormBuilder} from '@angular/forms';
import { animate } from '@angular/animations';

@Component({
  selector: 'app-pagepagination',
  templateUrl: './pagepagination.component.html',
  styleUrls: ['./pagepagination.component.css']
})
export class PagepaginationComponent implements OnInit {
  @Input('pageSize') 
    tableSize:any;
  url:string ="";
  count:any;
  constructor(private fb: FormBuilder, 
    private service: PageService, private router:Router){
      
     }
  ngOnInit(): void {
    this.url = this.router.url.substring(1,this.router.url.length);
    this.url = this.url.charAt(0).toLocaleUpperCase()+this.url.slice(1);
    console.log('si-->',this.tableSize);
    this.getPage(this.url,this.tableSize);

  }
  getPage(url:any,tableSize:any){
    this.service.getPage(url,tableSize).subscribe(data=>{
      this.count=data;
      console.log(this.count)
    })
  }
}
