import { useCallback, useEffect, useMemo, useState } from 'react';
import './App.css';
import { Box, Container, debounce, Grid2, List, ListItem, Pagination, Stack, Typography } from '@mui/material';
import OrgCard from './components/orgCard';
import { Organization, SearchAudit } from './types';
import SearchField from './components/searchField';
import LoadingSkeleton from './components/loadingSkeleton';
import ReloadButton from './components/reloadButton';


function App() {
    const [organizations, setOrganizations] = useState<Organization[]>();
    const [searchText, setSearchText] = useState<string|null>(null);
    const [sugText, setSugText] = useState<string>("");
    const [page, setPage] = useState(1);
    const [pageCount, setPageCount] = useState<number>(1);
    const [pageSize, setPageSize] = useState(10);
    const [prevSearches, setPrevSearches] = useState<string[]>([]);
    const [loading, setLoading] = useState(false);


    const onChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setSearchText(e.target.value);
    };

    useEffect(() => {
        getTopFive();
    }, [])

    const sendRequest = useCallback(async (value: string, pageSize: number, pageIndex: number) => {
        setLoading(true);
        await getOrgs(value, pageSize, pageIndex);
        if (value != "" && prevSearches[prevSearches.length] != value)
            setPrevSearches([...prevSearches.slice(-4), value]);
        setLoading(false);
    }, [prevSearches]);

    useEffect(() => {
        setSearchText(sugText);
    }, [sugText])

    const debouncedSendRequest = useMemo(() => {
        return debounce(sendRequest, 1000);
    }, [sendRequest]);

    const handleChange = (event: React.ChangeEvent<unknown>, value: number) => {
        setPage(value);
    };

    useEffect(() => {

        if (searchText?.length > 3) {
            debouncedSendRequest(searchText, pageSize, page);
        }
    }, [searchText]);

    useEffect(() => {
        if (searchText?.length > 3)
            getOrgs(searchText, pageSize, page);
    }, [page]);

    return (
        <Container>

            <Stack spacing={2} sx={{ marginTop: "1rem" }}>
                <Box sx={{ position: "sticky", top: 0, backgroundColor: '#fff', zIndex: 1, paddingBottom: "1rem" }}>
                    <Typography variant="h5" component="div" sx={{ flexGrow: 1, marginBottom: "2rem" }}>
                        Tax-Exempt Orgs
                    </Typography>
                    <Grid2 display="flex" justifyContent="space-between">
                        <SearchField
                            onChange={onChange}
                            prevSearches={prevSearches}
                            value={searchText}
                            setValue={setSugText}
                        />

                        <ReloadButton />
                    </Grid2>
                </Box>
                <Box>
                    <Stack spacing={2}>
                        <List sx={{ width: '100%', bgcolor: 'background.paper' }}>
                            {loading ? <LoadingSkeleton /> : organizations?.length != 0 ? organizations?.map((data) => {
                                return <OrgCard {...data} />
                            }) : <ListItem>Not found</ListItem>}
                        </List>
                        {loading ? null : organizations != null ?
                            <Box sx={{ position: "sticky", bottom: 0, backgroundColor: '#fff', zIndex: 0, padding: "1rem", display: "flex", justifyContent:"center" }}>
                                <Pagination count={pageCount} page={page} onChange={handleChange} />
                            </Box>
                            : <Box />}
                    </Stack>

                </Box>
            </Stack>
        </Container>
    );

    async function getOrgs(searchText: string, pageSize: number, pageIndex: number) {
        const response = await fetch('organizations?' + new URLSearchParams({
            searchText: searchText,
            pageSize: pageSize.toString(),
            pageIndex: pageIndex.toString()
        }));
        const data = await response.json();
        setOrganizations(data.items);
        setPageCount(Math.ceil(data.totalRecords / pageSize));
    }
    async function getTopFive() {
        const response = await fetch('searchAudits?' + new URLSearchParams({
            top: "5"
        }));
        const data = await response.json();
        setPrevSearches(data.map((x : SearchAudit) => x.searchText));
    }
}

export default App;