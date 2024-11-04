import { ChangeDetectionStrategy, Component, inject, OnInit, signal, ViewEncapsulation } from '@angular/core';
import { SharedModule } from '../../shared.module';
import { VehicleModel } from '../../models/vehicle.model';
import { FlexiGridService, StateModel } from 'flexi-grid';
import { HttpService } from '../../services/http.service';
import { ODataResultModel } from '../../models/odata-result.model';
import { vehicleFileUrl } from '../../constants';
import { FlexiToastService } from 'flexi-toast';
import { ResultModel } from '../../models/result.model';

@Component({
  selector: 'app-vehicles',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './vehicles.component.html',
  styleUrl: './vehicles.component.css',
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export default class VehiclesComponent implements OnInit {
  data = signal<VehicleModel[]>([]);
  total = signal<number>(0);
  loading = signal<boolean>(false);
  state = signal<StateModel>(new StateModel());
  vehicleFileUrl = signal<string>(vehicleFileUrl);
  years = signal<number[]>([]);

  createModel = signal<VehicleModel>(new VehicleModel());
  createPopupVisible = signal<boolean>(false);

  #http = inject(HttpService);
  #grid = inject(FlexiGridService);
  #toast = inject(FlexiToastService);

  ngOnInit(): void {
    this.calculateYears();
    this.getAll();
  }

  calculateYears(){
    const date = new Date();
    const year = date.getFullYear();
    this.years.set([]);

    for (let i = 0; i <= year - 2000; i++) {
      this.years.update(prev => [...prev, 2000 + i]);
    }
  }

  getAll(){
    let enpoint = "vehicle-gate/api/Vehicles?$count=true&";
    const odataEnpoint = this.#grid.getODataEndpoint(this.state());
    enpoint += odataEnpoint;

    this.loading.set(true);
    this.#http.get<ODataResultModel<VehicleModel[]>>(enpoint,(res)=> {
      this.data.set(res.data!);
      this.total.set(res.total);
      this.loading.set(false);
    },()=> this.loading.set(false));
  }

  getFile(event:any){
    this.createModel().file = event.target.files[0];
  }

  dataStateChange(event: StateModel){
    this.state.set(event);
    this.getAll();
  }

  create(){
    if(!this.createModel().file){
      this.#toast.showToast("Hata!","Kapak resmi seçmelisiniz","error");
      return;
    }
    const formData = new FormData();
    formData.append("brand", this.createModel().brand);
    formData.append("model", this.createModel().model);
    formData.append("year", this.createModel().year.toString());
    formData.append("plate", this.createModel().plate);
    formData.append("dailyPrice", this.createModel().dailyPrice.toString());
    formData.append("file", this.createModel().file, this.createModel().file.name);

    let enpoint = "vehicle-gate/api/Vehicles";

    this.#http.post<ResultModel<string>>(enpoint,formData,(res)=> {
      this.#toast.showToast("Başarılı",res.data!);
      this.createModel.set(new VehicleModel());
      this.createPopupVisible.set(false);
      this.getAll();
    });
  }
}
