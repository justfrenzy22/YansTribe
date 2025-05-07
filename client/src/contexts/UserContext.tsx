"use client";

import { IBaseUser } from "@/types/IEssentialsUser";
import { IUser } from "@/types/IUser";
import React, { createContext } from "react";

export const UserContext = createContext<IBaseUser | null>(null);

export const UserProvider = ({
	children,
	user,
}: {
	children: React.ReactNode;
	user: IUser | IBaseUser | null;
}) => {
	return <UserContext.Provider value={user}>{children}</UserContext.Provider>;
};