"use client";

import IUserContext from "@/types/context/IUserContext";
import { createContext } from "react";

export const UserContext = createContext<IUserContext>({ user: null });
export const UserProvider = ({
	children,
	user,
}: {
	children: React.ReactNode;
	user: IUserContext["user"];
}) => {
	return (
		<UserContext.Provider value={{ user }}>{children}</UserContext.Provider>
	);
};

