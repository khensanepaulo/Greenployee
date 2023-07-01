export class Pageable<T> {
  totalRegisters: number;
  data: T[];

  constructor(totalRegisters: number, data: T[]) {
    this.totalRegisters = totalRegisters;
    this.data = data;
  }
}
