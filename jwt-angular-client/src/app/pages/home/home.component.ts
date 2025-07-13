import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProductService } from '../../services/product.service';
import { Product } from '../../interfaces/product.model';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
  products: Product[] = [];
  newProduct: Product = {
    productName: '',
    price: 0,
    description: '',
  };
  isEditing = false;
  userRole: string = '';

  constructor(
    private productService: ProductService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.fetchProducts();
    this.fetchUserRole();
  }

  fetchUserRole() {
    this.userService.getProfile().subscribe({
      next: (res) => (this.userRole = res.result.userRole),
      error: () => (this.userRole = ''),
    });
  }

  fetchProducts() {
    this.productService.getAllProducts().subscribe({
      next: (res) => (this.products = res.data),
      error: () => alert('Failed to fetch products'),
    });
  }

  addProduct() {
    if (this.isEditing && this.newProduct.productId) {
      this.productService.updateProduct(this.newProduct).subscribe({
        next: () => {
          alert('Product updated!');
          this.resetForm();
          this.fetchProducts();
        },
        error: () => alert('Update failed'),
      });
    } else {
      this.productService.addProduct(this.newProduct).subscribe({
        next: () => {
          alert('Product added!');
          this.resetForm();
          this.fetchProducts();
        },
        error: () => alert('Add failed'),
      });
    }
  }

  editProduct(product: Product) {
    this.newProduct = { ...product };
    this.isEditing = true;
  }

  deleteProduct(id: number | undefined) {
    if (!id) return;
    if (confirm('Are you sure you want to delete this product?')) {
      this.productService.deleteProduct(id).subscribe({
        next: () => {
          alert('Product deleted!');
          this.fetchProducts();
        },
        error: () => alert('Delete failed'),
      });
    }
  }

  resetForm() {
    this.newProduct = { productName: '', price: 0, description: '' };
    this.isEditing = false;
  }
}
