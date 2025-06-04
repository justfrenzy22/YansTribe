"use client";

import useIsMobile from "@/hooks/contexts/useIsMobile";
import { AppContextValue } from "@/types/AppContextValue";
import { usePathname } from "next/navigation";
import { createContext, useEffect, useState } from "react";

export const AppContext = createContext<AppContextValue | null>(null);

export const AppProvider = ({
	userAgent,
	children,
}: {
	userAgent: string;
	children: React.ReactNode;
	// pathname?: string;
}) => {
	const { isMobile } = useIsMobile(userAgent);
	const [currPage, setCurrPage] = useState<{
		page: string;
		username: string;
	} | null>({
		page: `home`,
		username: ``,
	});

	const path = usePathname();

	useEffect(() => {
		const segment = path.split(`/`)[1];

		if (segment?.startsWith(`@`)) {
			setCurrPage({
				page: `profile`,
				username: segment.slice(1),
			});
		} else {
			if (segment === ``) {
				setCurrPage({
					page: `home`,
					username: ``,
				});
			} else {
				setCurrPage({
					page: segment ?? `home`,
					username: ``,
				});
			}
		}
	}, [path]);

	return (
		<AppContext.Provider
			value={{
				isMobile: isMobile,
				page: currPage?.page ?? "home",
				username: currPage?.username,
				setCurrPage: setCurrPage,
			}}
		>
			{children}
		</AppContext.Provider>
	);
};
