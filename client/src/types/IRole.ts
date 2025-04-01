import { Role as roleType } from "@/enums/Role";

export type IRole = roleType.user | roleType.admin | roleType.superAdmin;
