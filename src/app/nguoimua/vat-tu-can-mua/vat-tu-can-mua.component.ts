import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NguoimuaService } from 'src/app/service/nguoimua/nguoimua.service';

@Component({
  selector: 'app-vat-tu-can-mua',
  templateUrl: './vat-tu-can-mua.component.html',
  styleUrls: ['./vat-tu-can-mua.component.css']
})
export class VatTuCanMuaComponent implements OnInit {
  @Output() DataVatTu: EventEmitter<any> = new EventEmitter();
  dsVatTu: any = [];
  counts: any;
  pg: any= 1;
  search: any;
  dsPhieu:any=[]
  constructor(private service: NguoimuaService, private router: Router, private fb: FormBuilder, private route: ActivatedRoute) { }
  ngOnInit(): void {
    this.Load(this.pg)
  }
  Load(pg: any) {
    this.service.GetVatTuChoMua(pg).subscribe((data: any) => {
      this.dsVatTu = data.value,
        this.counts = data.serializerSettings;
      console.log("ds vật tư chờ mua", data);
    }, err =>{
      console.error('err', err);
      if(err.status ==403){
        this.router.navigate(['/error']);
      }
    })
  }
  LoadSearch(pg: any, search: any) {
    this.service.SearchVatTuChoMua(pg, search).subscribe((data: any) => {
      this.dsVatTu = data.value,
        this.counts = data.serializerSettings;
      console.log("ds vật tư chờ mua search", data);
    }, err =>{
      console.error('err', err);
      if(err.status ==403){
        this.router.navigate(['/error']);
      }
    })
  }
  onTableDataChange(event: any) {
    console.log("even", event);

    this.pg = event;
    if (this.search == null || this.search.trim() === '') {
      this.Load(this.pg);
    }
    else {
      this.LoadSearch(this.pg, this.search)
    }
  }
  Search() {
    if (this.search == null || this.search.trim() === '') {
      this.ngOnInit();
    }
    else {
      this.LoadSearch(this.pg, this.search)
    }
  }
  CheckBox(val:any){
    var obj = val
    let find = this.dsPhieu.find((o:any)=>o.idChiTietPhieu == obj.idChiTietPhieu) 
    if(find == null){
      if(this.dsPhieu.length+1 <=10){
        this.dsPhieu.push(obj)  
      }else{
        alert('Số lượng vật tư đề nghị không lớn hơn 10')
      }
    }else{
      this.dsPhieu.splice(this.dsPhieu.indexOf(obj),1);
    }
  }
  SenData(){
    this.DataVatTu = this.dsPhieu;  
    console.log("dsdata",this.DataVatTu)
    console.log("dsdata",this.dsPhieu)
  }
}
