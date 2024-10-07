import { Accordion, AccordionDetails, AccordionSummary, Alert, CardContent, Divider, ListItem, Typography } from '@mui/material';
import Grid from '@mui/material/Grid2';
import { Organization } from '../types';
import { ExpandMore } from '@mui/icons-material';
import { Fragment } from 'react/jsx-runtime';

export default function OrgCard(data: Organization) {
    return (<Fragment>
        <ListItem key={data.id}>

            <Grid container spacing={2} sx={{ width: "100%" }}>
                <Grid size={8}>
                    <CardContent>
                        <Typography sx={{ color: 'text.secondary' }}>{data.ein}</Typography>

                        <Typography gutterBottom variant="h6" component="div">
                            {data.name}
                        </Typography>
                        <Typography variant="body2" sx={{ color: 'text.secondary' }}>
                            {data.city + ", "}
                            {data.state + " "}
                            {data.country}
                        </Typography>
                    </CardContent>
                </Grid>
                <Grid size={4}>
                    {data.deductibilityCodes?.map((code, index) => {
                        const ac = "panel" + index + "-content";
                        const id = "panel" + index + "-header";

                        return (<Accordion
                            defaultExpanded={index == 0 ? true : false}
                            disableGutters={true}
                            key={index}
                        >
                            <AccordionSummary
                                expandIcon={<ExpandMore />}
                                aria-controls={ac}
                                id={id}
                            >
                                <Typography>{code.code}</Typography>
                            </AccordionSummary>
                            <AccordionDetails>
                                <Typography variant="body2" sx={{ color: 'text.secondary', marginBottom: "1rem" }}>
                                    {code.orgType}
                                </Typography>
                                <Alert severity={code.deductibilityLimitation == "Depends on various factors" ? "warning" : "info"}>

                                    Limitation: {code.deductibilityLimitation}
                                </Alert>


                            </AccordionDetails>
                        </Accordion>);
                    })}
                </Grid>
            </Grid>


        </ListItem>
        <Divider component="li" />
    </Fragment>);
}