import { UserContext } from "@/contexts/UserContext";
import IUserContext from "@/types/context/IUserContext";
import { useContext } from "react";

export const useUser = (): IUserContext =>
	useContext<IUserContext>(UserContext);
