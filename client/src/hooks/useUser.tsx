import { UserContext } from "@/contexts/UserContext";
import { IBaseUser } from "@/types/IEssentialsUser";
import { useContext } from "react";

export const useUser = (): IBaseUser | null =>useContext<IBaseUser | null>(UserContext);

