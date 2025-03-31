# Yanstribe API Documentation

This repository contains the API documentation for **Yanstribe**, a clone of Instagram and Facebook. Yanstribe is a social media platform designed to mimic the functionalities of both platforms, with its own unique features. The API is built to support user authentication, content creation, social interactions, and much more.

## Postman Collection

You can explore, test, and interact with the Yanstribe API using the Postman collection linked below:

[Yanstribe API Postman Collection](https://galactic-escape-205994.postman.co/workspace/New-Team-Workspace~1fbee21e-9f5a-4ea4-8e84-e3dc20392f94/collection/31467064-47562de9-6315-4091-a9e5-ea6737c599e9?action=share&creator=31467064)

### Features:
- User Registration and Authentication (Login/Signup)
- Profile Management (View/Update/Delete User Profiles)
- Content Creation (Post Images, Videos, Text, etc.)
- Social Interactions (Follow, Like, Comment)
- Newsfeed Display (View posts from friends/followed users)
- Notifications and Messages (Real-time notifications for user actions)

## How to Use the Collection

1. Click the [link to the Postman Collection](https://galactic-escape-205994.postman.co/workspace/New-Team-Workspace~1fbee21e-9f5a-4ea4-8e84-e3dc20392f94/collection/31467064-47562de9-6315-4091-a9e5-ea6737c599e9?action=share&creator=31467064) to open it in Postman.
2. You can either fork the collection to your workspace or use it directly.
3. Each request is well-commented to help you understand its purpose.
4. Test different endpoints by sending requests and checking the responses.

## Getting Started

Before testing the API, you’ll need the following:
- A **Postman** account.
- **Postman app** installed (or use Postman’s web version).
- The **API Base URL** (provided by the backend team).

### API Endpoints

Below is an overview of the main endpoints that are included in the Postman collection:

- **POST** `/api/register` - Register a new user.
- **POST** `/api/login` - Login a user and receive an authentication token.
- **GET** `/api/profile/{userId}` - Fetch a user profile.
- **POST** `/api/post/create` - Create a new post (image/video/text).
- **GET** `/api/newsfeed` - View the user’s newsfeed.
- **POST** `/api/follow/{userId}` - Follow another user.
- **POST** `/api/like/{postId}` - Like a post.

### Testing Tips

- For authentication, use the **Login API** to retrieve a token.
- Make sure to include the **Authorization header** in requests requiring authentication (e.g., `Authorization: Bearer <your_token>`).
- Explore different scenarios like creating posts, following users, and testing the newsfeed functionality.

## Conclusion

This collection serves as the foundation for testing and interacting with the *Yanstribe* API. It provides all the necessary functionality to interact with the *Yanstribe* platform, mimicking the features of Instagram and Facebook.

Feel free to contribute, fork, or share this collection to test and improve the API!

---

**Note**: This documentation is a guide for interacting with the API using Postman. For a detailed technical specification or changes, refer to the internal API documentation or contact the development team.