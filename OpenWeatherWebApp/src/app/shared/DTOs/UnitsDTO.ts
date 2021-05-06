export class UnitsDTO
{
    constructor(
        public tempDefault:number, 
        public tempMetrics:number, 
        public tempImperial:number, 
        public tempMinDefault:number, 
        public tempMinMetrics:number, 
        public tempMinImperial:number, 
        public tempMaxDefault:number, 
        public tempMaxMetrics:number, 
        public tempMaxImperial:number,
        public feelsLikeDefault:number, 
        public feelsLikeMetrics:number, 
        public feelsLikeImperial:number
    ) {}
}