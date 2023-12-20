import { Component } from '@angular/core';
import { CalculoModel } from './models/app.model';
import { ApiService } from './api.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'B3.UI';
  calculo: CalculoModel = new CalculoModel(1, 2);

  valor_bruto = 0;
  valor_liquido = 0;
  erros = []

  constructor(private apiService: ApiService) {}

  chamarAPI(): void {
    this.apiService.calcular(this.calculo).subscribe(
      (dados) => {
        this.erros = []
        this.valor_bruto = dados.valorBruto;
        this.valor_liquido = dados.valorLiquido;
      },
      (erro) => {
        this.erros = erro.error
        this.valor_bruto = 0;
        this.valor_liquido = 0;
      }
    );
  }
}
