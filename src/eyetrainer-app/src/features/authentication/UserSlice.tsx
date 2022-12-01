import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { LoginUserResponse } from "../../contracts/user/LoginUserResponse";

interface User {
  userId: number;
  token: string;
  email: string;
  role: string;
}

const initialState: User = {
  userId: -1,
  token: "",
  email: "",
  role: "",
};

const UserSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    setToken(state, action: PayloadAction<string>): void {
      state.token = action.payload;
    },
    setEmail(state, action: PayloadAction<string>): void {
      state.email = action.payload;
    },
    setRole(state, action: PayloadAction<string>): void {
      state.role = action.payload;
    },
    setUserInfo(state, action: PayloadAction<LoginUserResponse>): void {
      state.userId = action.payload.userId;
      state.email = action.payload.name;
      state.role = action.payload.role;
      state.token = action.payload.token;
    },
    logOutUser(state): void {
      state.token = "";
      state.email = "";
      state.role = "";
      state.userId = -1;
    },
  },
});

export const { setToken, logOutUser, setEmail, setRole, setUserInfo } =
  UserSlice.actions;

export default UserSlice.reducer;
