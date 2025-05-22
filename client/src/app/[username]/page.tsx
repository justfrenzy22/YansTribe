import GetUserByUsername from "@/api/auth/GetProfileByUsername";
import { Navbar } from "@/components/custom/navbar";
import WaitLayout from "@/components/home/waiting-animation";
// import ProfileProviderWrapper, { UserProvider } from "@/contexts/UserContext";
import UseTokenHook from "@/hooks/contexts/useTokenHook";
import { IUser } from "@/types/IUser";

import FriendsCard from "@/components/profile/friends-section";
import PostFeed from "@/components/home/post-feed";
import ProfileSection from "@/components/profile/profile-section";
import { redirect } from "next/navigation";
import ProfileProviderWrapper from "@/contexts/ProfileContext";
import { UserProvider } from "@/contexts/UserContext";

const ProfilePage = async ({
	params,
}: {
	params: Promise<{ username: string }>;
}) => {
	const raw = decodeURIComponent((await params).username);
	const username = raw.slice(raw.indexOf("@") + 1);

	const data = (await GetUserByUsername(username, await UseTokenHook())) as {
		message: string;
		status: number;
		user: IUser;
		profile: IUser;
	};

	if (data.status === 400) {
		redirect(`/auth`);
	}

	return (
		<>
			<WaitLayout data={data}>
				<UserProvider user={data.user}>
					<ProfileProviderWrapper user={data.user} initProfile={data.profile}>
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
									<ProfileSection />
									<FriendsCard  />
									<PostFeed posts={data.profile.posts} />
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
					</ProfileProviderWrapper>
				</UserProvider>
			</WaitLayout>
		</>
	);
};

export default ProfilePage;
