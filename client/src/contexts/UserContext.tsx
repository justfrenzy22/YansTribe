"use client";

import { IBaseUser } from "@/types/IEssentialsUser";
import React, { createContext, useContext } from "react";

const UserContext = createContext<IBaseUser | null>(null);

export const UserProvider = ({
	children,
	user,
}: {
	children: React.ReactNode;
	user: IBaseUser | null;
}) => {
	return <UserContext.Provider value={user}>{children}</UserContext.Provider>;
};

export const useUser = (): IBaseUser | null => {
	const context = useContext(UserContext);

	if (context === undefined) {
		throw new Error("useUser must be used within a UserProvider");
	}
	return context;
};
