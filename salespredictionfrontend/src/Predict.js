import React,{Component} from 'react';
import {Table} from 'react-bootstrap';

import {Button,ButtonToolbar} from 'react-bootstrap';
import {AddModal} from './AddModal';
import {EditModal} from './EditModal';

export class Predict extends Component{

    constructor(props){
        super(props);
        this.state={predsales:[], addModalShow:false, editModalShow:false}
    }

    refreshList(){
        fetch(process.env.REACT_APP_API+'user')
        .then(response=>response.json())
        .then(data=>{
            this.setState({predsales:data});
        });
    }

    componentDidMount(){
        this.refreshList();
    }

    componentDidUpdate(){
        this.refreshList();
    }

    render(){
        const {predsales: psales}=this.state;
        let addModalClose=()=>this.setState({addModalShow:false});
        let editModalClose=()=>this.setState({editModalShow:false});
        return(
            <div >
                <Table className="mt-4" striped bordered hover size="sm">
                    <thead>
                        <tr>
                        <th>SalesId</th>
                        <th>Date</th>
                        <th>Sale</th>
                        </tr>
                    </thead>
                    <tbody>
                        {psales.map(sale=>
                            <tr key={sale.SalesId}>
                                <td>{sale.SalesId}</td>
                                <td>{sale.Dos}</td>
                                <td>{sale.Sales}</td>
                            </tr>)}
                    </tbody>

                </Table>      
                <ButtonToolbar>
                    <Button variant='primary' onClick={()=>this.setState({addModalShow:true})}>Import</Button>
                    &nbsp;&nbsp;&nbsp;
                    <Button variant='primary' onClick={()=>this.setState({editModalShow:true})}>Predict</Button>
                    <AddModal show={this.state.addModalShow}
                    onHide={addModalClose}/>
                    <EditModal show={this.state.editModalShow}
                    onHide={editModalClose}/>
                </ButtonToolbar>
            </div>
        )
    }
}