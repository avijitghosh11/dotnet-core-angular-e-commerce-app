import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Product } from '../../shared/models/product';
import { ProductItemComponent } from './product-item/product-item.component';
import { MatDialog } from '@angular/material/dialog';
import { MatButton } from '@angular/material/button';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatIcon } from '@angular/material/icon';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu'
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { SortOption } from '../../shared/models/sortOption';
import { ShopParam } from '../../shared/models/shopParam';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Pagination } from '../../shared/models/pagination';
import { FormsModule } from '@angular/forms';
import { EmptyStateComponent } from "../../shared/components/empty-state/empty-state.component";

@Component({
  selector: 'app-shop',
  imports: [
    ProductItemComponent,
    MatButton,
    MatIcon,
    MatMenu,
    MatSelectionList,
    MatMenuTrigger,
    MatListOption,
    MatPaginator,
    FormsModule,
    EmptyStateComponent
  ],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {
  private shopService = inject(ShopService);
  private dialogService = inject(MatDialog);
  products?: Pagination<Product>;
  sortOptions: SortOption[] = [
    { name: "Alphabetical", value: "name" },
    { name: "Price: Low-High", value: "priceAsc" },
    { name: "Price: High-Low", value: "priceDesc" },
  ]
  shopParam = new ShopParam();
  pageSizeOptions = [5, 10, 15, 20];

  ngOnInit(): void {
    this.initializeShop();
  }

  initializeShop(): void {
    this.shopService.getBrands();
    this.shopService.getTypes();
    this.getProducts();
  }

  resetFilters() {
    this.shopParam = new ShopParam();
    this.getProducts();
  }

  onSearchChange() {
    this.shopParam.pageIndex = 1;
    this.getProducts();
  }

  onSortChange(event: MatSelectionListChange) {
    const selectedOption = event.options[0];
    if (selectedOption) {
      this.shopParam.sort = selectedOption.value;
      this.shopParam.pageIndex = 1;
      this.getProducts();
    }
  }

  openFilterDialog() {
    const dialogRef = this.dialogService.open(FiltersDialogComponent, {
      minWidth: '500px',
      data: {
        selectedBrands: this.shopParam.brands,
        selectedTypes: this.shopParam.types
      }
    });

    dialogRef.afterClosed().subscribe({
      next: result => {
        if (result) {
          this.shopParam.brands = result.selectedBrands;
          this.shopParam.types = result.selectedTypes;
          this.shopParam.pageIndex = 1;
          this.getProducts();
        }
      }
    });
  }

  getProducts() {
    this.shopService.getProducts(this.shopParam).subscribe({
      next: response => this.products = response,
      error: error => console.log(error)
    })
  }

  handelPageEvent(event: PageEvent) {
    this.shopParam.pageIndex = event.pageIndex + 1;
    this.shopParam.pageSize = event.pageSize;
    this.getProducts();
  }
}
