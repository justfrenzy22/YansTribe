import Header from "@/components/auth/header";
import WaitLayout from "@/components/auth/waiting-animation";
// import { headers } from "next/headers";
import Footer from "@/components/auth/footer";
import Brand from "@/components/auth/brand";
import RightMenu from "@/components/auth/right-menu";
import getUserById from "@/api/auth/getUserById";
import UseTokenHook from "@/hooks/contexts/useTokenHook";
import { IBaseUser } from "@/types/IBaseUser";
// import { AppProvider } from "@/contexts/AppContext";

const Auth = async () => {
	const data: {
		message: string;
		status: number;
		user: IBaseUser | null;
	} = await getUserById(await UseTokenHook());

	// const userAgent = (await headers()).get("user-agent") || "";

	return (
			<WaitLayout data={data}>
				<div className="h-screen w-full flex flex-col">
					{/* Header */}
					<Header />
					{/* Main content */}
					<div className="flex-1 flex flex-col md:flex-row items-center justify-center p-4 md:p-8 gap-8">
						<Brand />
						<RightMenu />
					</div>
					<Footer />
				</div>
			</WaitLayout>
	);
};

export default Auth;
