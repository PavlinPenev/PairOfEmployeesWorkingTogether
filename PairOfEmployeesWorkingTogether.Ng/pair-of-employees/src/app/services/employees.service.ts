import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { EmployeeProjectTableData } from '../models/employee-project-table-data.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {
  baseUrl = 'https://localhost:7054';
  employeesControllerRoute = 'api/employees';

  constructor(private http: HttpClient) { }

  getEmployees(file: File): Observable<EmployeeProjectTableData[]> {
    const formData = new FormData();
    formData.append('csvFile', file);

    return this.http.post<EmployeeProjectTableData[]>(
        `${this.baseUrl}/${this.employeesControllerRoute}/get`, 
        formData
    );
  }
}
