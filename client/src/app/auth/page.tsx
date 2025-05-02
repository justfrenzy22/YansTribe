"use server";
import Header from "@/components/auth/header";
import WaitLayout from "@/components/auth/waiting-animation";
import DeviceProvider from "@/contexts/DeviceContext";
import { headers } from "next/headers";
import Footer from "@/components/auth/footer";
import Brand from "@/components/auth/brand";
import RightMenu from "@/components/auth/right-menu";

const Auth = async () => {
	const userAgent = (await headers()).get("user-agent") || "";

	return (
		<DeviceProvider userAgent={userAgent}>
			<WaitLayout>
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
		</DeviceProvider>
	);
};

export default Auth;
