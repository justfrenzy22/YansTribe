// import Footer from "@/components/auth/footer";
import getUserById from "@/api/auth/getUserById";
import { Navbar } from "@/components/custom/navbar";
import PostFeed from "@/components/home/post-feed";
import WaitLayout from "@/components/home/waiting-animation";
import DeviceProvider from "@/contexts/DeviceContext";
import { UserProvider } from "@/contexts/UserContext";
import UseTokenHook from "@/hooks/useTokenHook";
import { IBaseUser } from "@/types/IEssentialsUser";
import { headers } from "next/headers";

const Home = async () => {
	// const cookieStore = await cookies();

	const data: {
		message: string;
		status: number;
		user: IBaseUser;
	} = await getUserById(await UseTokenHook());

	console.log("data", data);

	const userAgent = (await headers()).get("user-agent") || "";

	return (
		<DeviceProvider userAgent={userAgent}>
			<WaitLayout data={data}>
				<UserProvider user={data.user}>
					<Navbar />
					<main className="container mx-auto pt-16 md:px-4 px-2">
						<div className="flex gap-4">
							{/* Left Sidebar - hidden on mobile */}
							<div className="hidden md:block w-1/4 lg:w-1/5">
								<div className="sticky top-20">
									{/* <Sidebar /> */}
									sidebar
								</div>
							</div>

							{/* Main Content */}
							<div className="w-full md:w-2/4 lg:w-3/5 space-y-0">
								{/* md:space-y-4  */}
								{/* <StoriesBar /> */}

								<PostFeed />
							</div>

							{/* Right Sidebar - hidden on mobile */}
							<div className="hidden md:block w-1/4 lg:w-1/5">
								<div className="sticky top-20">
									{/* <RightSidebar /> */}
									right bar
								</div>
							</div>
						</div>
					</main>
				</UserProvider>
			</WaitLayout>
		</DeviceProvider>
	);
};

export default Home;
