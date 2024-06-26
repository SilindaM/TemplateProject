import { PATH_PUBLIC, PATH_DASHBOARD } from "../routes/path";

// URLS
export const HOST_API_KEY = 'https://localhost:7148/api';
export const REGISTER_URL = '/Auth/register';
export const LOGIN_URL = '/Auth/login';
export const ME_URL = '/Auth/me';
export const USERS_LIST_URL = '/Auth/users';
export const UPDATE_ROLE_URL = '/Auth/UpdateRoles';
export const USERNAMES_LIST_URL = '/Auth/usernames';
export const ALL_MESSAGES_URL = '/Message';
export const CREATE_MESSAGE_URL = '/Message/create';
export const MY_MESSAGE_URL = '/Message/mine';
export const LOGS_URL = '/Logs';
export const MY_LOGS_URL = '/Logs/mine';

// Auth Routes
export const PATH_AFTER_REGISTER = PATH_PUBLIC.login;
export const PATH_AFTER_LOGIN = PATH_DASHBOARD.dashboard;
export const PATH_AFTER_LOGOUT = PATH_PUBLIC.home;
