export enum Role {
    User = 0,
    Admin = 1
}

export class RouteValues {
    static readonly DEFAULT = ''
    static readonly LOGIN = 'login'
    static readonly REGISTER = 'register'
    static readonly ADMINISTRATE_TASKS = 'administrate-tasks'
    static readonly Task_NEW = 'task/new'
    static readonly Task_ID = 'task/:id'
    static readonly Task = 'task'
    static readonly DASHBOARD = 'dashboard'
    static readonly NOT_FOUND = 'not-found'
};

export class RouteEndpoints {
    static readonly Task = 'Task'
    static readonly USER = 'User'
    static readonly PRESENCE = 'TaskPresence'
    static readonly PRESENCE_GET_PARTICIPANTS = 'TaskPresence/GetParticipants'
};

