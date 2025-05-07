import { DeviceContext } from "@/contexts/DeviceContext";
import { useContext } from "react";

export const useDevice = () => {
	return useContext(DeviceContext);
};
