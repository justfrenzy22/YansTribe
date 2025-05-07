import GetUserByUsername from "@/api/auth/GetProfileByUsername";
import { Navbar } from "@/components/custom/navbar";
import WaitLayout from "@/components/home/waiting-animation";
import DeviceProvider from "@/contexts/DeviceContext";
import { UserProvider } from "@/contexts/UserContext";
import UseTokenHook from "@/hooks/useTokenHook";
import { IUser } from "@/types/IUser";
import { headers } from "next/headers";

const ProfilePage = async ({
	params,
}: {
	params: Promise<{ username: string }>;
}) => {
	const raw = decodeURIComponent((await params).username);
	const username = raw.slice(raw.indexOf("@") + 1);

	const data: {
		message: string;
		status: number;
		user: IUser;
	} = await GetUserByUsername(username, await UseTokenHook());

	const userAgent = (await headers()).get(`user-agent`) || ``;
	console.log("User-Agent:", userAgent);

	return (
		<DeviceProvider userAgent={userAgent}>
			<WaitLayout data={data}>
				<UserProvider user={data.user}>
					<Navbar />
					<p>Username: {username}</p>
				</UserProvider>
			</WaitLayout>
		</DeviceProvider>
	);
};

export default ProfilePage;
