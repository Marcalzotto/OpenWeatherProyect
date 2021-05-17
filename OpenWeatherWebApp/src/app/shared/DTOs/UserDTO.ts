export class UserDTO{
   
    constructor(
        public name:string,
        public surname:string,
        public email:string,
        public birthdate:Date,
        public password:string
    ) {}
}