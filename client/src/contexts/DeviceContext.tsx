"use client";

import useIsMobile from "@/hooks/useIsMobile";
import { DeviceContextValue } from "@/types/AppContextValue";
import { createContext } from "react";

export const DeviceContext = createContext<DeviceContextValue | null>(null);

const DeviceProvider = ({
	children,
	userAgent,
}: {
	children: React.ReactNode;
	userAgent: string;
}) => {
	const { isMobile } = useIsMobile(userAgent);

	return (
		<DeviceContext.Provider value={{ isMobile }}>
			{children}
		</DeviceContext.Provider>
	);
};
export default DeviceProvider;
