import { ProfileContext } from "@/contexts/ProfileContext";
import IProfileContext from "@/types/context/IProfileContext";
import { useContext } from "react";

export const useProfile = (): IProfileContext =>
	useContext<IProfileContext>(ProfileContext);
