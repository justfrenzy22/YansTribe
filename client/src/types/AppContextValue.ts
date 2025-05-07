export interface AppContextValue {
	page: string | null;
	username?: string | null;
	isMobile: boolean;
	setCurrPage: React.Dispatch<
		React.SetStateAction<{ page: string; username: string } | null>
	>;
}
