import { Component, OnInit } from '@angular/core';
import * as Constants from '../constants';
import { EmployeeProjectTableData } from '../models/employee-project-table-data.model';
import { MatTableDataSource } from '@angular/material/table';
import { EmployeesService } from '../services/employees.service';

@Component({
  selector: 'app-employees-dashboard',
  templateUrl: './employees-dashboard.component.html',
  styleUrls: ['./employees-dashboard.component.scss']
})
export class EmployeesDashboardComponent implements OnInit {
  textConstants = Constants;
  employeesLoaded = false;
  employeesProjects: EmployeeProjectTableData[] = [];
  dataSource: MatTableDataSource<EmployeeProjectTableData> = new MatTableDataSource<EmployeeProjectTableData>();
  
  displayedColumns = [
    'firstEmployeeId',
    'secondEmployeeId',
    'projectId',
    'daysWorkedTogether'
  ];

  constructor(private employeesService: EmployeesService) {}

  ngOnInit(): void {
  }

  csvInputChange(event: any) {
    this.getEmployees(event.target.files[0]);
  }

  private getEmployees(file: File): void {
    this.employeesService
      .getEmployees(file)
      .subscribe((response: EmployeeProjectTableData[]) => {
        this.employeesProjects = response;
        this.dataSource.data = this.employeesProjects;

        this.employeesLoaded = true;
      })
  }
}
