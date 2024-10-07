import { Divider, ListItem, Skeleton } from "@mui/material";
import { Fragment } from "react/jsx-runtime";

export default function LoadingSkeleton() {

    return (<Fragment>
        {[...new Array(5)].map(() => {
            return (
                <Fragment>
                    <ListItem>
                        <Skeleton width={"100%"} height={176} />
                    </ListItem>
                    <Divider component="li" />
                </Fragment>);
        })}
    </Fragment>);
}