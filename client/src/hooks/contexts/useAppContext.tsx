import { AppContext } from "@/contexts/AppContext";
import { AppContextValue } from "@/types/AppContextValue";
import { useContext } from "react";

export const useAppContext = (): AppContextValue | null =>
	useContext(AppContext);
