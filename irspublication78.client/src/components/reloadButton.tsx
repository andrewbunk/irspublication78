import { Sync } from "@mui/icons-material";
import { Backdrop, Button, CircularProgress, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle } from "@mui/material";
import { useState, useEffect, useRef } from "react";
import { Fragment } from "react/jsx-runtime";

export default function ReloadButton() {
    const [openDialog, setOpenDialog] = useState(false);
    const [openBackdrop, setOpenBackdrop] = useState(false);
    const descriptionElementRef = useRef<HTMLElement>(null);

    const handleDialogClose = () => {
        setOpenDialog(false);
    };

    const handleBackdropClose = () => {
        setOpenBackdrop(false);
    };

    const handleProceed = async () => {

        handleDialogClose();
        setOpenBackdrop(true);
        await reloadDb();
    };

    useEffect(() => {
        if (openDialog) {
            const { current: descriptionElement } = descriptionElementRef;
            if (descriptionElement !== null) {
                descriptionElement.focus();
            }
        }
    }, [openDialog]);
    return (
        <Fragment>
            <Button variant="contained" onClick={() => setOpenDialog(true)} endIcon={<Sync />}>
                Reload DB
            </Button>
            <Dialog
                open={openDialog}
                onClose={handleDialogClose}
                scroll="paper"
                aria-labelledby="scroll-dialog-title"
                aria-describedby="scroll-dialog-description"
                fullWidth={true}
            >
                <DialogTitle id="scroll-dialog-title">Reload DB</DialogTitle>
                <DialogContent dividers={true}>
                    <DialogContentText
                        id="scroll-dialog-description"
                        ref={descriptionElementRef}
                        tabIndex={-1}
                    >
                        Are you sure you want to reload the db from the IRS? This will take around 30 seconds.
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleDialogClose}>Cancel</Button>
                    <Button onClick={handleProceed}>Proceed</Button>
                </DialogActions>
            </Dialog>
            <Backdrop
                sx={(theme) => ({ color: '#fff', zIndex: theme.zIndex.drawer + 1 })}
                open={openBackdrop}
            >
                <CircularProgress color="inherit" />
            </Backdrop>
        </Fragment>);

    async function reloadDb() {
        const response = await fetch('organizations/load', {
            method: "POST"
        });
        const data = await response.json();
        if (data) {
            handleBackdropClose();
            handleDialogClose();
        }
    }
}