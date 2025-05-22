import type { NextConfig } from "next";
import { URL } from "url";

const url = process.env.NEXT_PUBLIC_CSHARP_API_BASE_URL
	? new URL(process.env.NEXT_PUBLIC_CSHARP_API_BASE_URL)
	: null;

const nextConfig: NextConfig = {
	pageExtensions: ["ts", "tsx"],
	images: {
		remotePatterns: url
			? [
					{
						protocol: url.protocol.replace(":", "") as "http" | "https", // e.g. "http"
						hostname: url.hostname, // e.g. "localhost"
						port: url.port, // e.g. "5114"
						pathname: "/cdn/**", // adjust as needed
					},
]
			: [],
	},
};

export default nextConfig;
