export interface User {
    id?:number,
    name: string,
    email: string,
    password: string,
    token?: string
}

export interface UserDetails {
    id: number,
    name: string,
    email: string,
    phoneNo: string,
    position: string
}
export interface UserUpdate {
    id: number,
    name: string,
    phoneNo: string,
    position: string
}

export interface UserUpdatePresence{
    id:number,
    isPresent:boolean,
}