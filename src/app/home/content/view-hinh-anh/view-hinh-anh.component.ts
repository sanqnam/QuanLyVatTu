import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-view-hinh-anh',
  templateUrl: './view-hinh-anh.component.html',
  styleUrls: ['./view-hinh-anh.component.css']
})
export class ViewHinhAnhComponent implements OnInit{
  @Input() dsImg: any[] = [];
  @Input() currentIndex:any;
  currentImage: any;

 url:string='../assets/img/image/vattu/vattu_default.jpg'
  constructor() { }

  ngOnInit() {
    this.showImage();
  }

  prevImage() {
    if (this.currentIndex > 0) {
      this.currentIndex--;
      this.showImage();
    }
  }

  nextImage() {
    if (this.currentIndex < this.dsImg.length - 1) {
      this.currentIndex++;
      this.showImage();
    }
  }

  showImage() {
    this.currentImage = this.dsImg[this.currentIndex];
    console.log("cuển img1 ", this.currentImage)
  }
}
