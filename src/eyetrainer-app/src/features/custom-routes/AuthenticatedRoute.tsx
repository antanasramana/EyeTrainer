import React, { ReactElement, useEffect, useState } from "react";
import { Navigate, Outlet } from "react-router-dom";
import { useAppSelector } from "../../app/hooks";

const AuthenticatedRoute: React.FC = (): ReactElement => {
  const userToken = useAppSelector((state): string => state.user.token);
  const [isAuthenticatedUser, setIsAuthenticatedUser] = useState<boolean>(true);

  useEffect((): void => {
    const isAuthenticated = userToken !== "";
    setIsAuthenticatedUser(isAuthenticated);
  }, [userToken]);

  return isAuthenticatedUser ? (
    <Outlet />
  ) : (
    <Navigate to="/SignIn" replace={true} />
  );
};

export default AuthenticatedRoute;
