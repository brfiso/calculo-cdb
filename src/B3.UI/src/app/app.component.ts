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

  valor_bruto = '';
  valor_liquido = '';
  erros = []

  constructor(private apiService: ApiService) {}

  formatarValorMonetario(valor: number): string {
    return valor.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL', minimumFractionDigits: 2, maximumFractionDigits: 2 });
  }

  chamarAPI(): void {
    
    this.apiService.calcular(this.calculo).subscribe(
      (dados) => {
        this.erros = []
        this.valor_bruto = this.formatarValorMonetario(dados.valorBruto);
        this.valor_liquido = this.formatarValorMonetario(dados.valorLiquido);
      },
      (erro) => {
        this.erros = erro.error
        this.valor_bruto = '';
        this.valor_liquido = '';
      }
    );
  }
}
