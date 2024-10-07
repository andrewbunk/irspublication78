import { Search } from "@mui/icons-material";
import { TextField, InputAdornment, List, ListItem, ListItemButton, ListItemText, ListSubheader, Box, Stack, Popover, Dialog, DialogContent, DialogContentText, DialogTitle, Divider } from "@mui/material";
import { Fragment, useEffect, useRef, useState } from "react";
import { SearchAudit } from "../types";

export default function SearchField(props) {
    const [anchorEl, setAnchorEl] = useState<HTMLDivElement | null>(null);
    const { onChange, prevSearches, value, setValue } = props;
    const [openDialog, setOpenDialog] = useState(false);
    const descriptionElementRef = useRef<HTMLElement>(null);
    const [searchList, setSearchList] = useState<SearchAudit[]>();

    const handleClick = (event: React.MouseEvent<HTMLDivElement>) => {
        if (prevSearches.length > 0)
            setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    useEffect(() => { handleClose(); }, [value]);

    const open = Boolean(anchorEl);

    const handleDialogClose = () => {
        setOpenDialog(false);
    };
    useEffect(() => {
        if (openDialog) {
            getSearchAudit();
            const { current: descriptionElement } = descriptionElementRef;
            if (descriptionElement !== null) {
                descriptionElement.focus();
            }
        }
    }, [openDialog]);

    return (
        <Stack>
            <TextField
                name="search"
                type="search"
                id="outlined-basic"
                value={value}
                variant="outlined"
                onChange={onChange}
                onClick={handleClick}
                autoComplete="false"
                fullWidth
                slotProps={{
                    //htmlInput: {
                    //    onclick:{ handleClick }
                    //},
                    input: {
                        endAdornment: (
                            <InputAdornment position="end">
                                <Search />
                            </InputAdornment>
                        ),
                    },
                }}>

            </TextField>
            <Popover
                open={open}
                anchorEl={anchorEl}
                onClose={handleClose}
                anchorOrigin={{
                    vertical: 'bottom',
                    horizontal: 'left',
                }}
                disableAutoFocus={true}
                disableEnforceFocus={true}
            >
                <Box sx={{ boxShadow: 3 }}>
                    <List
                        sx={{ width: '100%', maxWidth: 360, bgcolor: 'background.paper' }}
                        component="nav"
                        aria-labelledby="nested-list-subheader"
                        subheader={
                            <ListSubheader component="div" id="nested-list-subheader">
                                Previous searches
                            </ListSubheader>
                        }
                    >
                        {prevSearches.map((text: string) => {
                            return (<ListItem disablePadding>
                                <ListItemButton onClick={() => { setValue(text); setAnchorEl(null); }}>
                                    <ListItemText primary={text} />
                                </ListItemButton>
                            </ListItem>)
                        })}
                        <ListItem>
                            <ListItemButton onClick={() => setOpenDialog(true)}>
                                <ListItemText primary="See more" />
                            </ListItemButton>
                        </ListItem>
                    </List>
                </Box>
            </Popover>
            <Dialog
                open={openDialog}
                onClose={handleDialogClose}
                scroll="paper"
                aria-labelledby="scroll-dialog-title"
                aria-describedby="scroll-dialog-description"
                fullWidth={true}
            >
                <DialogTitle id="scroll-dialog-title">Previous Searches</DialogTitle>
                <DialogContent dividers={true}>
                    <DialogContentText
                        id="scroll-dialog-description"
                        ref={descriptionElementRef}
                        tabIndex={-1}
                    >
                        <List sx={{ width: '100%', bgcolor: 'background.paper' }}>
                            {searchList?.map(
                                (item) => <Fragment>
                                    <ListItem key={item.id}>
                                        <ListItemButton onClick={() => { setValue(item.searchText); setAnchorEl(null); handleDialogClose(); }}>
                                            <ListItemText primary={"Searched: " + item.searchText} secondary={"Total records: " + item.totalRecords} />
                                        </ListItemButton>
                                    </ListItem>
                                    <Divider component="li" />
                                </Fragment>)
                            }
                        </List>
                    </DialogContentText>
                </DialogContent>
            </Dialog>
        </Stack>
    );
    async function getSearchAudit() {
        const response = await fetch('searchAudits');
        const data = await response.json();
        setSearchList(data);
    }
    
}
